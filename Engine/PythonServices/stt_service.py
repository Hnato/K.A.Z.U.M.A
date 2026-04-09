from flask import Flask, request, jsonify
# import whisper

app = Flask(__name__)

@app.route("/transcribe", methods=["POST"])
def transcribe():
    data = request.json
    file_path = data.get("file", "")
    # model = whisper.load_model("base")
    # result = model.transcribe(file_path)
    return jsonify({"text": "To jest przyk┼éadowa transkrypcja."})

if __name__ == "__main__":
    app.run(port=5002)
