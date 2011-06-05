using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using SeasideResearch.LibCurlNet;
using System.Threading;

namespace OpenCharas
{
	public static class Updater
	{
		public struct Version
		{
			public int Major { get; set; }
			public int Minor { get; set; }
			public int Build { get; set; }

			public Version(string v) :
				this()
			{
				var s = v.Split('.');

				Major = int.Parse(s[0]);
				Minor = int.Parse(s[1]);
				Build = int.Parse(s[2] + s[3]);
			}

			public static bool operator==(Version lV, Version rV)
			{
				return lV.Major == rV.Major && lV.Minor == rV.Minor && lV.Build == rV.Build;
			}

			public static bool operator!=(Version lV, Version rV)
			{
				return !(lV == rV);
			}

			public static bool operator>(Version lV, Version rV)
			{
				if (lV.Major > rV.Major)
					return true;
				else if (lV.Major == rV.Major)
				{
					if (lV.Minor > rV.Minor)
						return true;
					else if (lV.Minor == rV.Minor)
					{
						if (lV.Build > rV.Build)
							return true;
					}
				}

				return false;
			}

			public static bool operator<(Version lV, Version rV)
			{
				return !(lV > rV || lV == rV);
			}

			public static bool operator<=(Version lV, Version rV)
			{
				return lV < rV || lV == rV;
			}

			public static bool operator>=(Version lV, Version rV)
			{
				return lV > rV || lV == rV;
			}

			public override bool Equals(object obj)
			{
				if (obj == null || !(obj is Version))
					return false;

				return (Version)obj == this;
			}

			public override int GetHashCode()
			{
				return Major.GetHashCode() + Minor.GetHashCode() + Build.GetHashCode();
			}

			public override string ToString()
			{
				return Major.ToString() + Minor.ToString() + Build.ToString("00");
			}
		}

		public struct Update
		{
			public Version Version { get; set; }
			public bool Major { get; set; }
			public string Description { get; set; }
			public string Date { get; set; }

			public Update(Version version, bool major, string description, string date) :
				this()
			{
				Version = version;
				Major = major;
				Description = description;
				Date = date;
			}

			public override string ToString()
			{
				return Version.Major.ToString() + "." + Version.Minor.ToString() + "." + Version.Build.ToString("00") + " [" + Date.ToString() + "]";
			}
		}

		public class PercentClass
		{
			public double dlNow, dlTotal;
		}

		static bool _curlInited = false;
		static MemoryStream DownloadFile(string url, Action<PercentClass> percUpd)
		{
			if (!_curlInited)
			{
				_curlInited = true;
				Curl.GlobalInit((int)CURLinitFlag.CURL_GLOBAL_DEFAULT);
			}

			Easy easy = new Easy();
			{
				PercentClass perc = new PercentClass();
				var stream = new System.IO.MemoryStream();
				easy.SetOpt(CURLoption.CURLOPT_URL, url);
				easy.SetOpt(CURLoption.CURLOPT_MAXREDIRS, 10);
				easy.SetOpt(CURLoption.CURLOPT_HEADER, false);
				easy.SetOpt(CURLoption.CURLOPT_CONNECTTIMEOUT, 45);
				easy.SetOpt(CURLoption.CURLOPT_TIMEOUT, 45);
				easy.SetOpt(CURLoption.CURLOPT_FOLLOWLOCATION, true);
				easy.SetOpt(CURLoption.CURLOPT_NOPROGRESS, false);
				easy.SetOpt(CURLoption.CURLOPT_PROGRESSDATA, perc);
				easy.SetOpt(CURLoption.CURLOPT_PROGRESSFUNCTION, (Easy.ProgressFunction)((extraData, dlTotal, dlNow, ulTotal, ulNow) =>
				{
					perc.dlTotal = dlTotal;
					perc.dlNow = dlNow;

					if (percUpd != null)
						percUpd(perc);

					return 0;
				})
				);
				easy.SetOpt(CURLoption.CURLOPT_WRITEFUNCTION, (Easy.WriteFunction)((buf, size, nmemb, extraData) =>
				{
					size *= nmemb;
					stream.Write(buf, 0, size);
					return size;
				})
				);

				var code = easy.Perform();
				easy.Cleanup();
				stream.Position = 0;

				if (code == CURLcode.CURLE_OK)
					return stream;
			}

			throw new Exception();
		}

		static List<Thread> _threads = new List<Thread>();

		public static void DownloadFileAsync(string url, Action<MemoryStream> finished, Action<PercentClass> percUpd)
		{
			Thread thread = new Thread((obj) =>
			{
				Thread t = (Thread)obj;

				lock (_threads)
					_threads.Add(t);

				using (var m = DownloadFile(url, percUpd))
					finished(m);

				lock (_threads)
					_threads.Remove(t);
			});
			thread.Start(thread);
		}

		public static List<Update> GetUpdates(StreamReader reader, bool onlyNew)
		{
			try
			{
				var myVer = new Version(Application.ProductVersion);
				var updates = new List<Update>();

				while (!reader.EndOfStream)
				{
					Update upd = new Update();

					upd.Version = new Version(reader.ReadLine());
					upd.Date = reader.ReadLine();
					upd.Major = bool.Parse(reader.ReadLine());
					upd.Description = reader.ReadLine();

					if (!reader.EndOfStream)
						reader.ReadLine();

					if (onlyNew && upd.Version <= myVer)
						break;

					updates.Add(upd);
				}

				return updates;
			}
			catch
			{
				return null;
			}
		}

		public static void CheckUpdates(Action<List<Update>> updatesFound, Action<PercentClass> percUpd, Action updatesNotFound)
		{
			Thread thread = new Thread((obj) =>
			{
				Thread t = (Thread)obj;

				lock (_threads)
					_threads.Add(t);

				using (var m = DownloadFile("http://opencharas.alteredsoftworks.com/updater/updates", percUpd))
				{
					using (var file = System.IO.File.Create("version.tmp"))
						m.CopyTo(file);

					using (var r = new StreamReader(m))
					{
						var upd = GetUpdates(r, false);

						if (upd.Count > 0 && upd[0].Version > new Version(Application.ProductVersion))
							updatesFound(upd);
						else
							updatesNotFound();
					}
				}

				lock (_threads)
					_threads.Remove(t);
			});
			thread.Start(thread);
		}

		public static void EndThreads()
		{
			foreach (var t in _threads)
				t.Abort();
		}
	}
}
