FROM microsoft/dotnet:2.1-sdk-alpine AS build
LABEL stage intermediate
WORKDIR /
COPY Outloud.Common/src src
COPY Outloud.Common/tests tests
RUN dotnet restore src/Outloud.Common.csproj
WORKDIR /src
# build
RUN dotnet build Outloud.Common.csproj -c Release -o /app
# publish
FROM build AS publish
ARG version=1
RUN dotnet pack /p:PackageVersion=1.0.${version} --no-restore -o /app
# copy tests
FROM build AS test
WORKDIR /tests
ADD https://github.com/ufoscout/docker-compose-wait/releases/download/2.3.0/wait /wait
RUN chmod +x /wait
ENTRYPOINT /wait && dotnet test --logger:trx
# push
FROM build as push
ARG apikey=insertkeyhere
ENV APIKEY=${apikey}
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT dotnet nuget push *.nupkg -k ${APIKEY} -s https://www.myget.org/F/outloud/api/v2/package