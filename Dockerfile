FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
RUN apt-get update -y
RUN apt-get install -y build-essential libz-dev
WORKDIR /App
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release --property:PublishDir=out

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /App
COPY --from=build-env /App/out .
CMD ["./MyFirstBot"]
