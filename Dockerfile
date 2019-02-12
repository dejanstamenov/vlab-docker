FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app/solution/
COPY ./vlab-docker-dotnet-core-stream-app/. /app/solution/
RUN dotnet clean \
    && dotnet build \
    && dotnet publish ./vlab-docker-dotnet-core-stream-app.csproj --runtime linux-x64 --configuration Release --self-contained \
    && cp -r ./bin/Release/netcoreapp2.2/linux-x64/publish/. ../
WORKDIR /app/
RUN rm -rd /app/solution/ \
    && chmod +x vlab-docker-dotnet-core-stream-app

FROM ubuntu:18.04
LABEL   maintainer="Dejan Stamenov" \
        maintainer_email="stamenov.dejan@outlook.com" \
        version="1.2"
WORKDIR /app/
COPY --from=build-env /app/. ./
RUN apt-get update \
    && apt-get --ignore-missing install -y \
            curl \
    && apt-get clean && rm -rf /var/lib/apt/lists/* \
EXPOSE 80/tcp