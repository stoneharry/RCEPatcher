# RCEPatcher
In the 3.3.5a WoW WOTLK client there is a Remote Code Exploit (RCE) that allows any private server owner to inject and run arbitrary code on your computer. This patcher will modify your WoW executable file to fix the exploit.

The program can be downloaded from the releases.

Simply drag and drop your `WoW.exe` game file onto the `RCEPatcher.exe`. This will work for servers with custom executables too. It creates a copy of the file with the file name ending in `_RCE_fix.exe`, i.e: `WoW.exe` would be patched to `WoW_RCE_fix.exe`.

If the patched file already exists or the executable is already patched, then the program will do nothing.

You can get output by running the program from command prompt.

![screenshot](https://i.imgur.com/NmsPYGm.png)

When running the patched executable, the client will crash when a server tries to use the exploit:

![crash](https://i.imgur.com/Fg4UpNR.png)
