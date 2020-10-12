import requests
import os
from dotenv import load_dotenv


load_dotenv(verbose=True)


base_url = 'http://localhost:5000/'

def post_letter(username, dialog, options={}):
    append_url = 'letter/add'
    voice_type = 'mijin'

    datas = {'username': username, 'dialog': dialog, 'voice_type': voice_type}
    datas.update(options)
    r = requests.post(f'{base_url}{append_url}', data=datas)
    print(r.content)
