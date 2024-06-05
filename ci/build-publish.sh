#!/bin/bash

: "${USERNAME:?USERNAME not set or empty}"
: "${REPO:?REPO not set or empty}"
: "${TAG:?TAG not set or empty}"

BUILDER_NAME="mybuilder"
docker buildx create --name $BUILDER_NAME --use

if [ -z "$1" ]; then
  echo "Build context not provided. Usage: ./build.sh <context> [extra buildx build arguments]"
  exit 1
fi

docker buildx build \
    --platform=linux/amd64,linux/arm64 \
    -t "${USERNAME}/${REPO}:${TAG}" \
    -t "${USERNAME}/${REPO}:latest" \
    --push \
    "$1" \
    "${@:2}"

docker buildx rm $BUILDER_NAME