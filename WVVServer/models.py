from app import db
from sqlalchemy.dialects.postgresql import JSON


class CustomEnv(db.Model):
    # 유저는 자신의 환경 세팅을 등록할 수 있다.
    __tablename__ = 'custom_envs'

    id = db.Column(db.Integer, primary_key=True)
    username = db.Column(db.String())
    desc = db.Column(db.String())
    tag = db.Column(db.String())
    setting = db.Column(JSON)

    def __init__(self, username, desc, tag, setting):
        self.username = username
        self.desc = desc
        self.tag = tag
        self.setting = setting

    def __repr__(self):
        return '<id {}>'.format(self.id)


class Letter(db.Model):
    # 유저는 자신의 편지를 보낼 수 있다.
    __tablename__ = 'letters'

    id = db.Column(db.Integer, primary_key=True)
    username = db.Column(db.String())
    dialog = db.Column(db.String())
    file_path = db.Column(db.String())

    def __init__(self, username, dialog, file_path):
        self.username = username
        self.dialog = dialog
        self.file_path = file_path

    def __repr__(self):
        return '<id {}>'.format(self.id)