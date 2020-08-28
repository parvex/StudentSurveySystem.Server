import time
import os
import json
from locust import HttpUser, task, between
from locust.env import Environment

iterator = 0

class SwarmUser(HttpUser):
    wait_time = between(1, 2)
    host = "https://localhost:5001"
    auth_header = None

    @task
    def survey_forms(self):
        self.client.get("/Surveys/MyNotFilledForms")

    @task
    def survey_forms(self):
        self.client.get("/Surveys/3")

    @task
    def survey_forms(self):
        self.client.post("/SurveyResults/MyNotFilledForms")

    def on_start(self):
        self.client.verify = False
        global iterator
        iterator += 1
        print("USER " + str(iterator))
        self.client.get("/Users/UsosAuthData")
        data = {"username":"student" + str(iterator), "password":"password"}
        authResult = self.client.post("/Users/Authenticate", json=data)
        result = authResult.json()
        self.client.headers["Authorization"] = "Bearer " + result["token"]

if __name__ == "__main__":
    env = Environment()
    SwarmUser(env).run()