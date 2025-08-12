#!/bin/bash

echo "🔨 Building Docker image locally for testing..."

# 로컬에서 Docker 이미지 빌드 테스트
docker build -t nsxlibrarymanager-test:latest .

if [ $? -eq 0 ]; then
    echo "✅ Docker build successful!"
    echo "🚀 You can now run the container with:"
    echo "docker run -p 8080:8080 nsxlibrarymanager-test:latest"
else
    echo "❌ Docker build failed!"
    exit 1
fi