using System.Collections.Generic;
using System;
using System.Drawing;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace OpenCharas
{
	public class QColorMatrix
	{
		public QColorMatrix()
		{
			PI = (float)(4.0F * (float)(Math.Atan(1.0)));
			Rad = (float)(4.0F * (float)(Math.Atan(1.0))) / 180.0F;

		}

		float PI;
		float Rad;

		// The luminance weight factors for the RGB color space.
		// These values are actually preferable to the better known factors of
		// Y = 0.30R + 0.59G + 0.11B, the formula which is used in color television technique.
		const float lumR = 0.3086F;
		const float lumG = 0.6094F;
		const float lumB = 0.082F;

		static bool Initialized = false;
		static QColorMatrix PreHue = new QColorMatrix();
		static QColorMatrix PostHue = new QColorMatrix();

		private System.Drawing.Imaging.ColorMatrix MyMatrix_ = new System.Drawing.Imaging.ColorMatrix();
		public System.Drawing.Imaging.ColorMatrix MyMatrix
		{
			get { return MyMatrix_; }
			set { MyMatrix_ = value; }
		}

		private void Copy(QColorMatrix CopyFrom)
		{
			for (int x = 0; x <= 4; x++)
			{
				for (int y = 0; y <= 4; y++)
				{
					MyMatrix[x, y] = CopyFrom.MyMatrix[x, y];
				}
			}
		}

		public void Reset()
		{
			for (int x = 0; x <= 4; x++)
			{
				for (int y = 0; y <= 4; y++)
				{
					if (x == y)
						MyMatrix[x, y] = 1;
					else
						MyMatrix[x, y] = 0;
				}
			}
		}

		public void Multiply(QColorMatrix Matrix, MatrixOrder Order = MatrixOrder.Prepend)
		{
			// NOTE: The last column is NOT calculated, because it is (or at least should be)
			// always { 0, 0, 0, 0, 1 }.

			System.Drawing.Imaging.ColorMatrix A;
			System.Drawing.Imaging.ColorMatrix B;

			if (Order == MatrixOrder.Append)
			{
				A = Matrix.MyMatrix;
				B = MyMatrix;
			}
			else
			{
				A = MyMatrix;
				B = Matrix.MyMatrix;
			}

			System.Drawing.Imaging.ColorMatrix Temp = new System.Drawing.Imaging.ColorMatrix();

			for (int y = 0; y <= 4; y++)
			{
				for (int x = 0; x <= 3; x++)
				{
					float T = 0;

					for (int i = 0; i <= 4; i++)
						T += (float)(B[y, i] * A[i, x]);

					Temp[y, x] = T;
				}
			}

			for (int y = 0; y <= 4; y++)
			{
				for (int x = 0; x <= 3; x++)
					MyMatrix[y, x] = Temp[y, x];
			}
		}

		// Inverts the colors
		public void Invert()
		{
			MyMatrix[0, 0] = -1;
			MyMatrix[1, 1] = -1;
			MyMatrix[2, 2] = -1;
			MyMatrix[4, 0] = 1;
			MyMatrix[4, 1] = 1;
			MyMatrix[4, 2] = 1;
			MyMatrix[4, 4] = 1;
		}

		// Assumes that v points to (at least) four REALs.
		public void TransformVector(ref float[] V)
		{
			float[] Temp = new float[4];

			for (int x = 0; x <= 3; x++)
			{
				Temp[x] = MyMatrix[4, x];

				for (int y = 0; y <= 3; y++)
					Temp[x] += (float)(V[y] * MyMatrix[y, x]);
			}

			for (int x = 0; x <= 3; x++)
				V[x] = Temp[x];
		}

		public void TransformColors(List<Color> Colors)
		{
			float[] P = new float[4];

			for (int i = 0; i <= Colors.Count - 1; i++)
			{
				P[0] = (float)(Colors[i].R);
				P[1] = (float)(Colors[i].G);
				P[2] = (float)(Colors[i].B);
				P[3] = (float)(Colors[i].A);

				TransformVector(ref P);

				for (int j = 0; j <= 3; j++)
				{
					if (P[j] < 0)
						P[j] = (float)(0.0F);
					else if (P[j] > 255.0F)
						P[j] = (float)(255.0F);
				}

				Colors[i] = Color.FromArgb((byte)P[0], (byte)P[1], (byte)P[2], (byte)P[3]);
			}
		}

		// phi is in degrees
		// x and y are the indices of the value to receive the sin(phi) value
		protected void RotateColor(float Phi, int x, int y, MatrixOrder order = MatrixOrder.Prepend)
		{
			Phi *= Rad;

			QColorMatrix m = new QColorMatrix();
			m.MyMatrix[x, x] = (float)(Math.Cos(Phi));
			m.MyMatrix[y, y] = (float)(Math.Cos(Phi));

			float s = (float)(Math.Sin(Phi));
			m.MyMatrix[y, x] = s;
			m.MyMatrix[x, y] = -s;

			Multiply(m, order);
		}

		protected void ShearColor(int x, int y1, float d1, int y2, float d2, MatrixOrder order = MatrixOrder.Prepend)
		{
			QColorMatrix m = new QColorMatrix();
			m.MyMatrix[y1, x] = d1;
			m.MyMatrix[y2, x] = d2;

			Multiply(m, order);
		}

		public void Scale(float scaleRed, float scaleGreen, float scaleBlue, float scaleOpacity, MatrixOrder order = MatrixOrder.Prepend)
		{
			QColorMatrix m = new QColorMatrix();
			m.MyMatrix[0, 0] = scaleRed;
			m.MyMatrix[1, 1] = scaleGreen;
			m.MyMatrix[2, 2] = scaleBlue;
			m.MyMatrix[3, 3] = scaleOpacity;

			Multiply(m, order);
		}

		// Scale just the three colors with the same amount, leave opacity unchanged.
		public void ScaleColors(float ScaleAmount, MatrixOrder order = MatrixOrder.Prepend)
		{
			Scale(ScaleAmount, ScaleAmount, ScaleAmount, (float)(1.0F), order);
		}

		// Scale just the opacity, leave R, G and B unchanged.
		public void ScaleOpacity(float ScaleAmt, MatrixOrder order = MatrixOrder.Prepend)
		{
			Scale((float)(1.0F), (float)(1.0F), (float)(1.0F), ScaleAmt, order);
		}

		// Rotate the matrix around one of the color axes. The color of the rotation
		// axis is unchanged, the other two colors are rotated in color space.
		// The angle phi is in degrees (-180.0f... 180.0f).
		public void RotateRed(float Phi, MatrixOrder order = MatrixOrder.Prepend)
		{
			RotateColor(Phi, 2, 1, order);
		}

		public void RotateGreen(float Phi, MatrixOrder order = MatrixOrder.Prepend)
		{
			RotateColor(Phi, 0, 2, order);
		}

		public void RotateBlue(float Phi, MatrixOrder order = MatrixOrder.Prepend)
		{
			RotateColor(Phi, 1, 0, order);
		}

		// Shear the matrix in one of the color planes. The color of the color plane
		// is influenced by the two other colors.
		public void ShearRed(float Green, float Blue, MatrixOrder order = MatrixOrder.Prepend)
		{
			ShearColor(0, 1, Green, 2, Blue, order);
		}

		public void ShearGreen(float Red, float Blue, MatrixOrder order = MatrixOrder.Prepend)
		{
			ShearColor(1, 0, Red, 2, Blue, order);
		}

		public void ShearBlue(float Red, float Green, MatrixOrder order = MatrixOrder.Prepend)
		{
			ShearColor(2, 0, Red, 1, Green, order);
		}

		public void Translate(float offsetRed, float offsetGreen, float offsetBlue, float offsetOpacity, MatrixOrder order = MatrixOrder.Prepend)
		{
			QColorMatrix m = new QColorMatrix();
			m.MyMatrix[4, 0] = offsetRed;
			m.MyMatrix[4, 1] = offsetGreen;
			m.MyMatrix[4, 2] = offsetBlue;
			m.MyMatrix[4, 3] = offsetOpacity;

			Multiply(m, order);
		}

		// Translate just the three colors with the same amount, leave opacity unchanged.
		public void TranslateColors(float Offset, MatrixOrder order = MatrixOrder.Prepend)
		{
			Translate(Offset, Offset, Offset, (float)0.0, order);
		}

		// Translate just the opacity, leave R, G and B unchanged.
		public void TranslateOpacity(float OffsetOpacity, MatrixOrder order = MatrixOrder.Prepend)
		{
			Translate((float)(0.0F), (float)(0.0F), (float)(0.0F), OffsetOpacity, order);
		}

		public void SetSaturation(float Saturation, MatrixOrder order = MatrixOrder.Prepend)
		{
			// For the theory behind this, see the web sites at the top of this file.
			// In short: if saturation is 1.0f, m becomes the identity matrix, and this matrix is
			// unchanged. If saturation is 0.0f, each color is scaled by it's luminance weight.
			float SatCompl = (float)(1.0F - Saturation);
			float SatComplR = (float)(lumR * SatCompl);
			float SatComplG = (float)(lumG * SatCompl);
			float SatComplB = (float)(lumB * SatCompl);

			float[][] matrixValues = new float[][] { new Single[] { SatComplR + Saturation, SatComplR, SatComplR, 0, 0 }, new Single[] { SatComplG, SatComplG + Saturation, SatComplG, 0, 0 }, new Single[] { SatComplB, SatComplB, SatComplB + Saturation, 0, 0 }, new Single[] { 0, 0, 0, 1, 0 }, new Single[] { 0, 0, 0, 0, 1 } };

			QColorMatrix m = new QColorMatrix();
			m.MyMatrix = new System.Drawing.Imaging.ColorMatrix(matrixValues);

			Multiply(m, order);
		}

		public void RotateHue(float Phi)
		{
			InitHue();

			// Rotate the grey vector to the blue axis
			Multiply(PreHue, MatrixOrder.Append);

			// Rotate around the blue axis
			RotateBlue(Phi, MatrixOrder.Append);

			Multiply(PostHue, MatrixOrder.Append);
		}

		private static void InitHue()
		{
			const float greenRotation = 35.0F;

			// NOTE: theoretically, greenRotation should have the value of 39.182655 degrees,
			// being the angle for which the sine is 1/(sqrt(3)), and the cosine is sqrt(2/3).
			// However, I found that using a slightly smaller angle works better.
			// In particular, the greys in the image are not visibly affected with the smaller
			// angle, while they deviate a little bit with the theoretical value.
			// An explanation escapes me for now.
			// If you rather stick with the theory, change the comments in the previous lines.


			if (Initialized == false)
			{
				Initialized = true;
				// Rotating the hue of an image is a rather convoluted task, involving several matrix
				// multiplications. For efficiency, we prepare two static matrices.
				// This is by far the most complicated part of this class. For the background
				// theory, refer to the sgi-sites mentioned at the top of this file.

				// Prepare the preHue matrix.
				// Rotate the grey vector in the green plane.
				PreHue.RotateRed((float)(45.0F));

				// Next, rotate it again in the green plane, so it coincides with the blue axis.
				PreHue.RotateGreen((float)(-greenRotation), MatrixOrder.Append);

				// Hue rotations keep the color luminations constant, so that only the hues change
				// visible. To accomplish that, we shear the blue plane.
				float[] lum = new float[] { lumR, lumG, lumB, 1.0F };

				// Transform the luminance vector.
				PreHue.TransformVector(ref lum);

				// Calculate the shear factors for red and green.
				float red = (float)(lum[0] / lum[2]);
				float green = (float)(lum[1] / lum[2]);

				// Shear the blue plane.
				PreHue.ShearBlue(red, green, MatrixOrder.Append);

				// Prepare the postHue matrix. This holds the opposite transformations of the
				// preHue matrix. In fact, postHue is the inversion of preHue.
				PostHue.ShearBlue((float)(-red), (float)(-green));
				PostHue.RotateGreen((float)(greenRotation), MatrixOrder.Append);
				PostHue.RotateRed((float)-45.0F, MatrixOrder.Append);
			}
		}

	}
}
