; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{EA5E5C39-8D72-446B-9CAD-DE4D6BF14B8E}
AppName=OpenCharas
AppVersion=1.0
;AppVerName=OpenCharas 1.0
AppPublisher=Altered Softworks
AppPublisherURL=http://opencharas.alteredsoftworks.com/
AppSupportURL=http://opencharas.alteredsoftworks.com/
AppUpdatesURL=http://opencharas.alteredsoftworks.com/
DefaultDirName={pf}\OpenCharas
DefaultGroupName=OpenCharas
AllowNoIcons=yes
OutputBaseFilename=setup
Compression=lzma2
SolidCompression=yes

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "C:\projects\OpenCharas\bin\Debug\OpenCharas.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\projects\OpenCharas\bin\Debug\Associator.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\projects\OpenCharas\bin\Debug\pack.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\projects\OpenCharas\bin\Debug\sheet.ico"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\projects\OpenCharas\bin\Debug\Documentation\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\OpenCharas"; Filename: "{app}\OpenCharas.exe"
Name: "{group}\{cm:ProgramOnTheWeb,OpenCharas}"; Filename: "http://opencharas.alteredsoftworks.com/"
Name: "{group}\{cm:UninstallProgram,OpenCharas}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\OpenCharas"; Filename: "{app}\OpenCharas.exe"; Tasks: desktopicon
Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\OpenCharas"; Filename: "{app}\OpenCharas.exe"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\Associator.exe";
Filename: "{app}\OpenCharas.exe"; Description: "{cm:LaunchProgram,OpenCharas}"; Flags: nowait postinstall skipifsilent

