from app import db
from models import Letter, CustomEnv
from sqlalchemy.sql.expression import func, select


def get_letter_by_id(id):
    letter = Letter.query.filter_by(id=id).first()
    db.session.commit()
    db.session.remove()
    return letter

def get_letters():
    letters = Letter.query.all()
    db.session.commit()
    db.session.remove()
    return letters

def get_random_letter():
    letter = Letter.query.order_by(func.random()).first()
    db.session.commit()
    db.session.remove()
    return letter

def add_letter(username, dialog, dialog_data):
    letter = Letter(username=username, dialog=dialog, dialog_data=dialog_data)
    db.session.add(letter)

    db.session.commit()
    db.session.expunge_all()
    db.session.close()
    return letter

def get_env_by_id(id):
    env = CustomEnv.query.filter_by(id=id).first()
    db.session.commit()
    db.session.expunge_all()
    db.session.close()
    return env

def get_envs():
    envs = CustomEnv.query.all()

    db.session.commit()
    db.session.expunge_all()
    db.session.close()

    return envs

def add_env(username, desc, tag, setting):
    env = CustomEnv(username=username, desc=desc, tag=tag, setting=setting)

    db.session.add(env)
    db.session.expunge_all()
    db.session.close()
    db.session.commit()

