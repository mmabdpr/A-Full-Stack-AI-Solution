FROM conda/miniconda3
RUN conda config --env --add channels conda-forge
RUN conda install -y nltk pandas
RUN pip install Flask flask-restful
RUN mkdir -p /app
COPY ./models /app/models/
COPY ./api.py /app
COPY ./nltk_data /root/nltk_data
WORKDIR /app
EXPOSE 12000
CMD ["python", "api.py"]