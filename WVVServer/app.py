from flask import Flask
from flask_sqlalchemy import SQLAlchemy
from flask import request

import os

from dotenv import load_dotenv


load_dotenv(verbose=True, override=True)

app = Flask(__name__)
app.config.from_object(os.environ['APP_SETTINGS'])
app.config['SQLALCHEMY_TRACK_MODIFICATIONS'] = False
db = SQLAlchemy()
db.init_app(app)

import dbfunc
import models

@app.route('/letter/all', methods=['GET'])
def get_letters():
    # 추가구현 필요.
    return dbfunc.get_letters()

@app.route('/letter/<int:letter_id>')
def get_letter(letter_id):
    # 추가구현 필요.
    return dbfunc.get_letter_by_id(letter_id)

@app.route('/letter/add', methods=['POST'])
def post_letter():
    import clova
    username = request.values['username']
    dialog = request.values['dialog']
    voice_type = request.values['voice_type']
    option = {}
    if 'option' in request.values:
        option = request.values['option']

    success, dialog_data = clova.get_voice(voice_type, dialog, option)# 추후 채워야함.
    if success:
        dbfunc.add_letter(username, dialog, dialog_data)
        return {"success" : True}
    else:
        return {"success" : False, 'reason': dialog_data}

@app.route('/env/all', methods=['GET'])
def get_envs():
    # 추가구현 필요.
    return dbfunc.get_envs()

@app.route('/env/<int:env_id>', methods=['GET'])
def get_env(env_id):
    # 추가구현 필요.
    return dbfunc.get_env_by_id(env_id)

@app.route('/env/add', methods=['POST'])
def post_env():
    username = request.values['username']
    desc = request.values['desc']
    tag = request.values['tag']
    dbfunc.add_env(username, desc, tag)
    return {"success" : True}

if __name__ == '__main__':
    port = int(os.environ.get('PORT', 5000))
    app.run(host='0.0.0.0', port=port)
