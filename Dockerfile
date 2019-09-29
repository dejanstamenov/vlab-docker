FROM ubuntu:16.04
LABEL   maintainer="Dejan Stamenov" \
        maintainer_email="stamenov.dejan@outlook.com" \
        version="1.6"
RUN apt-get update \
    && apt-get --ignore-missing install -y \
        netcat-traditional \
    && apt-get clean && rm -rf /var/lib/apt/lists/*
EXPOSE 8080/tcp