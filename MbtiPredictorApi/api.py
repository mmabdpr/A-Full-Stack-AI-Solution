from flask import Flask
from flask_restful import Api, Resource, reqparse

import pandas as pd
import numpy as np
import nltk
import string
import pickle

with open('./models/IntroExtro.pickle', 'rb') as f:
    IntroExtro = pickle.load(f)
with open('./models/IntuitionSensing.pickle', 'rb') as f:
    IntuitionSensing = pickle.load(f)
with open('./models/ThinkingFeeling.pickle', 'rb') as f:
    ThinkingFeeling = pickle.load(f)
with open('./models/JudgingPercieiving.pickle', 'rb') as f:
    JudgingPercieiving = pickle.load(f)

useless_words = nltk.corpus.stopwords.words("english") + list(string.punctuation)
def build_bag_of_words_features_filtered(words):
    words = nltk.word_tokenize(words)
    return {
        word:1 for word in words \
        if not word in useless_words}

def MBTI(input):
    tokenize = build_bag_of_words_features_filtered(input)
    ie = IntroExtro.classify(tokenize)
    Is = IntuitionSensing.classify(tokenize)
    tf = ThinkingFeeling.classify(tokenize)
    jp = JudgingPercieiving.classify(tokenize)
    
    mbt = ''
    
    if(ie == 'introvert'):
        mbt+='I'
    if(ie == 'extrovert'):
        mbt+='E'
    if(Is == 'Intuition'):
        mbt+='N'
    if(Is == 'Sensing'):
        mbt+='S'
    if(tf == 'Thinking'):
        mbt+='T'
    if(tf == 'Feeling'):
        mbt+='F'
    if(jp == 'Judging'):
        mbt+='J'
    if(jp == 'Percieving'):
        mbt+='P'
    return(mbt)

def tellmemyMBTI(input):
    a = []
    trait1 = pd.DataFrame([0,0,0,0],['I','N','T','J'],['count'])
    trait2 = pd.DataFrame([0,0,0,0],['E','S','F','P'],['count'])
    for i in input:
        a += [MBTI(i)]
    for i in a:
        for j in ['I','N','T','J']:
            if(j in i):
                trait1.loc[j]+=1                
        for j in ['E','S','F','P']:
            if(j in i):
                trait2.loc[j]+=1 
    trait1 = trait1.T
    trait1 = trait1*100/len(input)
    trait2 = trait2.T
    trait2 = trait2*100/len(input)
    
    YourTrait = ''
    for i,j in zip(trait1,trait2):
        temp = max(trait1[i][0],trait2[j][0])
        if(trait1[i][0]==temp):
            YourTrait += i  
        if(trait2[j][0]==temp):
            YourTrait += j
    
    return YourTrait

app = Flask(__name__)
api = Api(app)

class PersonalityPrediction(Resource):
    def post(self):
        parser = reqparse.RequestParser()
        parser.add_argument("text")
        args = parser.parse_args()

        text = args['text']

        mbti = MBTI(text)

        result = {
            "prediction": mbti
        }

        return result, 200

api.add_resource(PersonalityPrediction, "/predict")
app.run(host='0.0.0.0', port=12000)
