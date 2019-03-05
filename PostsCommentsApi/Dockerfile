FROM microsoft/dotnet:2.2-aspnetcore-runtime-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk-stretch AS build
WORKDIR /src
COPY ["PostsCommentsApi/PostsCommentsApi.csproj", "PostsCommentsApi/"]
COPY ["DataLayer/DataLayer.csproj", "DataLayer/"]
COPY ["BusinessLayer/BusinessLayer.csproj", "BusinessLayer/"]
COPY ["Common/Common.csproj", "Common/"]
RUN dotnet restore "PostsCommentsApi/PostsCommentsApi.csproj"
COPY . .
WORKDIR "/src/PostsCommentsApi"
RUN dotnet build "PostsCommentsApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PostsCommentsApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PostsCommentsApi.dll"]
