# Repository information

This repository contains Ubuntu Docker image with Apache2 server which is being used in Docker Swarm, as by the provided `docker-compose.yml` file.

The idea is to use this base image and extend it to support creation of virtual laboratories. Currently, on the Apache2 server a static web site is being hosted. Essentially, the static web site should be replaced with web application that will be handling the tasks of the virtual laboratory.

# Docker build tags

Following Docker image tags are available:

- `latest` - this is the latest **stable** image from the `master` branch in the repository. May not be complete with all the features.
- `experimental` - use for testing purposes **only**. The image is most likely not stable.
- `v1.0` - **preffered** image version. This is a **stable** image with all the required features.