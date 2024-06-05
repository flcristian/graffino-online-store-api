#!/bin/bash

# Ensure mandatory environment variables are set
: "${USERNAME:?USERNAME not set or empty}"
: "${REPO:?REPO not set or empty}"
: "${TAG:?TAG not set or empty}"

# Source service_version_number.sh
source ./../service_version_number.sh

# Check if SERVICE_VERSION is defined
if [ -z "$SERVICE_VERSION" ]; then
    echo "Error: SERVICE_VERSION is not defined in service_version_number.sh."
    exit 1
fi

echo "Building Docker image..."
docker build -f Dockerfile -t ${REPO}:${SERVICE_VERSION} .

echo "Getting the new image ID..."
IMAGE_ID=$(docker images -q ${REPO}:${SERVICE_VERSION})
echo "New image ID: $IMAGE_ID"

echo "Tagging the image..."
docker tag ${REPO}:${SERVICE_VERSION} ${USERNAME}/${REPO}:${SERVICE_VERSION}
docker tag ${REPO}:${SERVICE_VERSION} ${USERNAME}/${REPO}:${TAG}

echo "Pushing the tagged image..."
docker push ${USERNAME}/${REPO}:${SERVICE_VERSION}
docker push ${USERNAME}/${REPO}:${TAG}

echo "Script completed."