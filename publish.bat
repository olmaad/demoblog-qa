echo Publishing backend
dotnet publish ./Backend/Backend.csproj -c Release -o ./../Docker/Build/Blog/Backend/BackendPublish

echo Publishing base builder
dotnet publish ./BaseBuilder/BaseBuilder.csproj -c Release -o ./../Docker/Build/Blog/Backend/BaseBuilderPublish

echo Copying builder test data
xcopy /y /i "TestData\builder.json" "Docker\Build\Blog\Backend\"

echo Publishing frontend
cd Frontend
cmd /c npm run build
cd ..
md Docker/Build/Blog/Proxy/Output
xcopy /y /e /i "Frontend\public" "Docker\Build\Blog\Proxy\Output"

echo Composing server docker
cd Docker/Build/Blog
docker-compose build
cd ../../..

echo Publishing tests
dotnet publish ./Tests/Tests.csproj -c Release -o ./../Docker/Build/Tests/Output

echo Publishing test data
xcopy /y /e /i "TestData" "Docker\Build\Tests\TestData"

echo Composing tests docker
cd Docker/Build/Tests
docker-compose  build
cd ../../..
