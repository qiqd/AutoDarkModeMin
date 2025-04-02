; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "AutoDarkModeMin"
#define MyAppVersion "1.1.2"
#define MyAppExeName "AutoDarkModeMin.exe"
#define MyAppAssocName MyAppName + " File"
#define MyAppAssocExt ".myp"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{EE9657D8-3BFC-41FD-8B05-CD1EBB879B85}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
DefaultDirName={autopf}\{#MyAppName}
UninstallDisplayIcon={app}\{#MyAppExeName}
ChangesAssociations=yes
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only).
;PrivilegesRequired=lowest
OutputDir=D:\Quick access\Desktop
OutputBaseFilename=AutoDarkModeMinSetup
SetupIconFile=D:\Quick access\Desktop\code\.net\AutoDarkModeMin\Resources\logo.ico
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "chinesesimplified"; MessagesFile: "compiler:Languages\ChineseSimplified.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "D:\Quick access\Desktop\code\.net\AutoDarkModeMin\bin\Release\net8.0-windows\publish\win-x86\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Quick access\Desktop\code\.net\AutoDarkModeMin\bin\Release\net8.0-windows\publish\win-x86\AutoDarkModeMin.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Quick access\Desktop\code\.net\AutoDarkModeMin\bin\Release\net8.0-windows\publish\win-x86\AutoDarkModeMin.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Quick access\Desktop\code\.net\AutoDarkModeMin\bin\Release\net8.0-windows\publish\win-x86\AutoDarkModeMin.dll.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Quick access\Desktop\code\.net\AutoDarkModeMin\bin\Release\net8.0-windows\publish\win-x86\AutoDarkModeMin.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Quick access\Desktop\code\.net\AutoDarkModeMin\bin\Release\net8.0-windows\publish\win-x86\Microsoft.Extensions.Logging.Abstractions.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "D:\Quick access\Desktop\code\.net\AutoDarkModeMin\bin\Release\net8.0-windows\publish\win-x86\Quartz.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Registry]
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocExt}\OpenWithProgids"; ValueType: string; ValueName: "{#MyAppAssocKey}"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}"; ValueType: string; ValueName: ""; ValueData: "{#MyAppAssocName}"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#MyAppExeName},0"
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

