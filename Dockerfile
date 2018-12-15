FROM ubuntu:18.04

LABEL   maintainer="Dejan Stamenov" \
        maintainer_email="stamenov.dejan@outlook.com"

RUN apt-get update && apt-get install -y apache2 && apt-get clean && rm -rf /var/lib/apt/lists/*

EXPOSE 80/tcp
EXPOSE 8080/tcp

CMD ["/usr/sbin/apache2", "-D", "FOREGROUND"]