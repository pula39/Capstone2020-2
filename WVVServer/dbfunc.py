from app import db
from models import Letter, CustomEnv

def get_letter_by_id(id):
    return Letter.query.filter_by(id=id).first()

def get_letters():
    return Letter.query.all()

def add_letter(username, dialog, dialog_data):
    letter = Letter(username=username, dialog=dialog, dialog_data=dialog_data)
    db.session.add(letter)
    db.session.commit()

def get_env_by_id(id):
    return CustomEnv.query.filter_by(id=id).first()

def get_envs():
    return CustomEnv.query.all()

def add_env(username, desc, tag, setting):
    env = CustomEnv(username=username, desc=desc, tag=tag, setting=setting)
    db.session.add(env)
    db.session.commit()

