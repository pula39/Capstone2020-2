import requests
import os

'''
speaker	string	음성 합성에 사용할 목소리 종류

mijin : 미진 : 한국어, 여성 음색
jinho : 진호 : 한국어, 남성 음색
shinji : 신지: 일본어, 남성 음색

clara : 클라라 : 영어, 여성 음색
matt : 매트 : 영어, 남성 음색
meimei : 메이메이 : 중국어, 여성 음색
liangliang : 리앙리앙 : 중국어, 남성 음색
jose : 호세 : 스페인어, 남성 음색
carmen : 카르멘 : 스페인어, 여성 음색

text	string	음성 합성할 문장. UTF-8 인코딩된 텍스트만 지원합니다. 최대 1000 자의 텍스트까지 음성 합성을 지원합니다. 한 문장에서 사용할 수 있는 최대 글자는 200글자 입니다. 기호나 괄호 안의 텍스트는 읽지 않습니다.	없음	Y
volume	integer	음성 볼륨. -5에서 5 사이의 정수 값이며, -5 이면 0.5 배 낮은 볼륨이고 5 이면 1.5 배 더 큰 볼륨입니다. 0 이면 정상 볼륨의 목소리로 음성을 합성합니다.	0	N
speed	integer	음성 속도. -5에서 5 사이의 정수 값이며, -5 이면 2 배 빠른 속도이고 5 이면 0.5 배 더 느린 속도입니다. 0 이면 정상 속도의 목소리로 음성을 합성합니다.	0	N
pitch	integer	음성 피치. -5에서 5 사이의 정수 값이며, -5 이면 1.2 배 높은 피치이고 5 이면 0.8 배 더 낮은 피치입니다. 0 이면 정상 피치의 목소리로 음성을 합성합니다.	0	N
format	string	음성 포멧. mp3를 입력하세요.	mp3	N
alpha	string	음색 변환. -5에서 5 사이의 정수 값이며, -5 이면 1.2 배 높은 음색이고 5 이면 0.8 배 더 낮은 음색입니다. 0 이면 정상 음색의 목소리로 음성을 합성합니다.
'''

class VOICETYPE():
    MIJIN = "mijin"
    JINHO = "jinho"
    SHINJI = "shinji"

def get_voice(voice_type, text, options = {}):
    url = 'https://naveropenapi.apigw.ntruss.com/tts-basic/v1/tts'
    client_id = os.environ['CLOVA_CLIENT_ID']
    client_secret = os.environ['CLOVA_CLIENT_SECRET']
    headers = {'X-NCP-APIGW-API-KEY-ID': client_id,
               'X-NCP-APIGW-API-KEY': client_secret,
               "Content-Type": "application/x-www-form-urlencoded"}
    datas = {'speaker':voice_type, 'text': text, 'format':'wav'}
    datas.update(options)
    r = requests.post(url, data=datas, headers=headers)
    if r.status_code != 200:
        print(os.getcwd())
        with open(f'error.txt', 'wb') as f:
            f.write(r.content)
        return False, r.content

    with open(f'{voice_type}_{text[:50]}.wav', 'wb') as f:
        f.write(r.content)

    return True, r.content


