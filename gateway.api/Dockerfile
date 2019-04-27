#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see http://aka.ms/containercompat 

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY gateway.api/gateway.api.csproj gateway.api/
RUN dotnet restore gateway.api/gateway.api.csproj
COPY . .
WORKDIR /src/gateway.api
RUN dotnet build gateway.api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish gateway.api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "gateway.api.dll"]
