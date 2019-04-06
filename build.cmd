pushd docker
for /f %%i in ('dir /b') do (
  pushd %%i
  docker build -t pro/%%i --pull .
  popd
)
popd
