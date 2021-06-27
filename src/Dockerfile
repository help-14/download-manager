FROM --platform=arm64 mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM --platform=arm64 mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./DownloadManager.csproj"
RUN dotnet publish -c release --self-contained -o /build

FROM base AS final
WORKDIR /app
COPY --from=build /build .
ENTRYPOINT [ "dotnet", "DownloadManager" ]