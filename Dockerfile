FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app/solution/
COPY ./vlab-docker-dotnet-core-simple-web-api/. /app/solution/
RUN dotnet clean \
    && dotnet build \
    && dotnet publish ./vlab-docker-dotnet-core-simple-web-api.csproj --runtime linux-x64 --configuration Release --self-contained \
    && cp -r ./bin/Release/netcoreapp2.2/linux-x64/publish/. ../
WORKDIR /app/
RUN rm -rd /app/solution/ \
    && chmod +x vlab-docker-dotnet-core-simple-web-api

FROM ubuntu:18.04
LABEL   maintainer="Dejan Stamenov" \
        maintainer_email="stamenov.dejan@outlook.com" \
        version="1.3"
WORKDIR /app/
COPY --from=build-env /app/. ./
RUN apt-get update \
    && apt-get --ignore-missing install -y \
            curl \
            libssl1.0.0 \
    && apt-get clean && rm -rf /var/lib/apt/lists/* \
EXPOSE 5000/tcp 5001/tcp