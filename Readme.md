# Overview
Predicts your MBTI label based on the text you write. The goal of this project is getting experience with deployment of an AI solution. Theoretical aspect of ML is not part of this project.

## What I've done

- Implemented a full-fledged solution with the goal of gaining experience with deployment of AI-based applications.
- Crawled data of fictional and non-fictional characters with their MBTI label from a website using F\#.
- Trained and saved an NLTK Naive Bayes model using the crawled dataset.
- Implemented a simple Web API using Flask to serve the saved model.
- Designed a Web UI using ASP.NET Blazor for users to interact with the application.
- Dockerized the project to easily run it in any environment.

## Usage
- setup docker properly
- check firewall
    - on fedora/centos (change zone and subnet based on your setup):

        ```bash
        firewall-cmd --permanent --zone=FedoraWorkstation --add-rich-rule='rule family=ipv4 source address=172.22.0.0/16 accept'
        
        firewall-cmd --zone=FedoraWorkstation --add-masquerade --permanent
        
        firewall-cmd --reload
        ```
- publish dotnet project

    ```dotnetcli
    cd MbtiPredictorGUI
    dotnet publish -c Release
    ```
- use docker compose to build and run docker containers

    ```bash
    docker-compose up
    ```
- browse this url: localhost:8000

## Screenshots

![screenshot-1](https://github.com/mohammadmahdiabdollahpour/mbti_predictor_blazor_nltk/raw/master/screenshots/1.jpg "screenshot-1")

![screenshot-2](https://github.com/mohammadmahdiabdollahpour/mbti_predictor_blazor_nltk/raw/master/screenshots/2.jpg "screenshot-2")

![screenshot-3](https://github.com/mohammadmahdiabdollahpour/mbti_predictor_blazor_nltk/raw/master/screenshots/3.jpg "screenshot-3")

## Notes & Acknowledgements
- The code to train the models has been written by [tgdivy](https://www.kaggle.com/tgdivy/mbti-personality-classifier)
- Characters' data have been collected from personality-database.com
