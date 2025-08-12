#!/usr/bin/env pwsh

Write-Host "🔍 Testing Docker build..." -ForegroundColor Yellow

try {
    # Docker 빌드 테스트
    docker build --no-cache -t nsxlibrarymanager-test:latest .
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "✅ Build successful!" -ForegroundColor Green
        Write-Host "🚀 Ready for deployment!" -ForegroundColor Cyan
    } else {
        Write-Host "❌ Build failed!" -ForegroundColor Red
        exit 1
    }
} catch {
    Write-Host "❌ Error during build: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}