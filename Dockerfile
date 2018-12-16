FROM ubuntu:18.04

LABEL   maintainer="Dejan Stamenov" \
        maintainer_email="stamenov.dejan@outlook.com"

RUN apt-get update && apt-get install -y apache2 && apt-get clean && rm -rf /var/lib/apt/lists/*

ENV APACHE_RUN_USER www-data
ENV APACHE_RUN_GROUP www-data
ENV APACHE_LOG_DIR /var/log/apache2

COPY ./vlab-docker-app/. /var/www/html/

EXPOSE 80/tcp
# To expose 8080/tcp port on the Apache server, the following files must be modified:
#   - /etc/apache2/ports.conf
#   - /etc/apache2/sites-enabled/000-default.conf
#EXPOSE 8080/tcp

CMD ["apachectl", "-D", "FOREGROUND"]