from flask import Flask, request, jsonify
from transformers import MarianMTModel, MarianTokenizer

app = Flask(__name__)

# Helsinki-NLP/opus-mt-pl-en
model_name = "Helsinki-NLP/opus-mt-pl-en"
tokenizer = MarianTokenizer.from_pretrained(model_name)
model = MarianMTModel.from_pretrained(model_name)

@app.route("/translate", methods=["POST"])
def translate():
    data = request.json
    text = data.get("text", "")
    source = data.get("source", "pl")
    target = data.get("target", "en")
    
    # Simple logic for now, in production you'd select the model based on source/target
    tokens = tokenizer([text], return_tensors="pt", padding=True)
    translated = model.generate(**tokens)
    result = tokenizer.batch_decode(translated, skip_special_tokens=True)
    return jsonify({"translated": result[0]})

if __name__ == "__main__":
    app.run(port=5001)
