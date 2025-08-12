#!/usr/bin/env pwsh

Write-Host "🔨 Building Docker image locally for testing..." -ForegroundColor Yellow

# 로컬에서 Docker 이미지 빌드 테스트
docker build -t nsxlibrarymanager-test:latest .

if ($LASTEXITCODE -eq 0) {
    Write-Host "✅ Docker build successful!" -ForegroundColor Green
    Write-Host "🚀 You can now run the container with:" -ForegroundColor Cyan
    Write-Host "docker run -p 8080:8080 nsxlibrarymanager-test:latest" -ForegroundColor White
} else {
    Write-Host "❌ Docker build failed!" -ForegroundColor Red
    exit 1
}