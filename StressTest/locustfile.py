import time
import os
import json
from locust import HttpUser, task, between
from locust.env import Environment

iterator = 0

class SwarmUser(HttpUser):
    wait_time = between(2, 4)
    host = "https://localhost:5001"
    auth_header = None


    @task(2)
    def survey_form_list0(self):
        self.client.get("/Surveys/MyNotFilledForms?page=0")

    @task(1)
    def survey_form_list1(self):
        self.client.get("/Surveys/MyNotFilledForms?page=1")

    @task(1)
    def survey_form_list2(self):
        self.client.get("/Surveys/MyNotFilledForms?page=2")

    @task(2)
    def survey_form(self):
        self.client.get("/Surveys/3")

    @task
    def add_survey_result(self):
        survey_result = {"Id":None,"Answers":[{"Id":None,"Value":"5","QuestionText":None,"QuestionType":3,"QuestionId":8},{"Id":None,"Value":"11/13/2020 00:00:00","QuestionText":None,"QuestionType":4,"QuestionId":9}],"SurveyId":3}
        self.client.post("/SurveyResponses", json=survey_result)

    def on_start(self):
        self.client.verify = False
        global iterator
        iterator += 1
        # self.client.get("/Users/UsosAuthData")
        data = {"username":"student" + str(iterator), "password":"password"}
        authResult = self.client.post("/Users/Authenticate", json=data)
        result = authResult.json()
        self.client.headers["Authorization"] = "Bearer " + result["token"]

if __name__ == "__main__":
    env = Environment()
    SwarmUser(env).run()