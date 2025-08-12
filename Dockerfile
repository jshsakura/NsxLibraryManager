FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt-get update && apt-get install -y --no-install-recommends gosu && rm -rf /var/lib/apt/lists/*
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
WORKDIR /src
COPY . .
RUN dotnet restore "src/NsxLibraryManager/NsxLibraryManager.csproj"
WORKDIR "/src/src/NsxLibraryManager"
RUN dotnet build "NsxLibraryManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NsxLibraryManager.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
COPY --from=publish /app/publish /app
RUN mkdir -p /app/backup
RUN mkdir -p /app/renamer/in /app/renamer/out
RUN mkdir -p /app/wwwroot/images/icon

# 다국어 지원을 위한 로케일 설정
RUN apt-get update && apt-get install -y locales && rm -rf /var/lib/apt/lists/* \
    && sed -i '/en_US.UTF-8/s/^# //g' /etc/locale.gen \
    && sed -i '/ko_KR.UTF-8/s/^# //g' /etc/locale.gen \
    && locale-gen

ENV LANG=en_US.UTF-8
ENV LANGUAGE=en_US:en
ENV LC_ALL=en_US.UTF-8

COPY entrypoint.sh /app/entrypoint.sh
RUN chmod +x /app/entrypoint.sh

WORKDIR /app
ENTRYPOINT ["/app/entrypoint.sh"]