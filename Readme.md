Predicts your MBTI label based on the text you write
# Usage
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

# Screenshots

![screenshot-1](https://github.com/mohammadmahdiabdollahpour/mbti_predictor_blazor_nltk/raw/master/screenshots/1.png "screenshot-1")

![screenshot-2](https://github.com/mohammadmahdiabdollahpour/mbti_predictor_blazor_nltk/raw/master/screenshots/2.png "screenshot-2")

![screenshot-3](https://github.com/mohammadmahdiabdollahpour/mbti_predictor_blazor_nltk/raw/master/screenshots/3.png "screenshot-3")

# Notes & Acknowledgements
- The code to train the models has been written by [tgdivy](https://www.kaggle.com/tgdivy/mbti-personality-classifier)
- Characters' data have been collected from personality-database.com