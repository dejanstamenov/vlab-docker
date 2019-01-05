FROM ubuntu:18.04

LABEL   maintainer="Dejan Stamenov" \
        maintainer_email="stamenov.dejan@outlook.com" \
        version="1.2"

RUN apt-get update \
    && apt-get --ignore-missing install -y \
            curl \
            wget \
            software-properties-common \
            apt-transport-https \
    && wget -q https://packages.microsoft.com/config/ubuntu/18.04/packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && add-apt-repository universe \
    && apt-get update \
    && apt-get install aspnetcore-runtime-2.2 \
    && apt-get clean && rm -rf /var/lib/apt/lists/* \
    && mkdir /app /app/solution/

COPY ./vlab-docker-dotnet-core-stream-app/. /app/solution/

WORKDIR /app/

RUN dotnet clean \
    && dotnet build \
    && dotnet publish ./solution/vlab-docker-dotnet-core-stream-app.csproj --runtime linux-x64 --configuration Release --self-contained

COPY ./solution/bin/Release/netcoreapp2.2/linux-x64/publish/. ../

RUN rm -rd /app/solution/ \
    && chmod +x vlab-docker-dotnet-core-stream-app

EXPOSE 80/tcp

CMD nohup ./vlab-docker-dotnet-core-stream-app 80 > app.log &