import ollama

# Analiz edilecek resmin tam yolu (M3 Mac'inizdeki yola göre güncelleyin)
resim_yolu = '/kullanicilar/kullaniciadi/Masaustu/ornek_resim.jpg'

print("Resim analiz ediliyor, lütfen bekleyin...")

# Model adını kurulu olan vision modelinizle değiştirin (örn: qwen2.5-vl, llava)
response = ollama.chat(
    model='qwen2.5-vl', 
    messages=[{
        'role': 'user',
        'content': 'Bu resimde ne görüyorsun? Lütfen detaylıca açıkla.',
        'images': [resim_yolu]
    }]
)

print("\n--- Modelin Yanıtı ---")
print(response['message']['content'])
