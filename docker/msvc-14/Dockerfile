# escape=`

FROM mcr.microsoft.com/windows/servercore:ltsc2019

ADD https://go.microsoft.com/fwlink/p/?LinkID=2033908 C:\TEMP\winsdksetup.exe
RUN C:\TEMP\winsdksetup.exe /norestart /quiet /features OptionId.DesktopCPPx86

ADD https://aka.ms/vs/15/release/vs_buildtools.exe C:\TEMP\vs_buildtools.exe
RUN C:\TEMP\vs_buildtools.exe --quiet --wait --norestart --nocache --locale en-US `
    --add Microsoft.VisualStudio.Workload.VCTools `
#    --add Microsoft.VisualStudio.Component.Windows10SDK.17134 `
    --installPath C:\BuildTools

ENTRYPOINT C:\BuildTools\Common7\Tools\VsDevCmd.bat > nul &&
CMD ["cmd"]
