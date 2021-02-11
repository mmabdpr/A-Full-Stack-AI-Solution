FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY MbtiPredictor/bin/Release/net5.0/publish /app
WORKDIR /app
COPY MBTIData /data
ENV MBTI_DATA /data
CMD [ "./MbtiPredictor" ]