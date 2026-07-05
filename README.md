from openai import OpenAI
# Configured by environment variables
client = OpenAI()

messages = [
    {
        "role": "user",
        "content": [
            {
                "type": "image_url",
                "image_url": {
                    "url": "https://qianwen-res.oss-accelerate.aliyuncs.com/Qwen3.5/demo/CI_Demo/mathv-1327.jpg"
                }
            },
            {
                "type": "text",
                "text": "The centres of the four illustrated circles are in the corners of the square. The two big circles touch each other and also the two little circles. With which factor do you have to multiply the radii of the little circles to obtain the radius of the big circles?\nChoices:\n(A) $\\frac{2}{9}$\n(B) $\\sqrt{5}$\n(C) $0.8 \\cdot \\pi$\n(D) 2.5\n(E) $1+\\sqrt{2}$"
            }
        ]
    }
]

chat_response = client.chat.completions.create(
    model="Qwen/Qwen3.6-35B-A3B",
    messages=messages,
    max_tokens=81920,
    temperature=1.0,
    top_p=0.95,
    presence_penalty=1.5,
    extra_body={
        "top_k": 20,
    }, 
)
print("Chat response:", chat_response)

------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------
------------------------------------------------------------------------------------------------------------------------------------------------


The Catch: Vision in GGUF
Unlike MLX or standard Hugging Face formats that bundle the vision and text architectures together, the GGUF ecosystem splits multimodal models into separate files. The unsloth/Qwen3.6-35B-A3B-MTP-GGUF repository contains only the main text model.

To perform image analysis, you must also download the corresponding vision projector file (usually named with mmproj, such as mmproj-Qwen3.6-35B-A3B-f16.gguf). If you attempt to pass an image to the MTP GGUF without the projector file, the model will fail or hallucinate because it has no "eyes."

Using it in your Jupyter Environment
Since you want to work in Jupyter, you should use llama-cpp-python with Apple Silicon's Metal backend enabled so it leverages your M3 Ultra's GPU cores.

1. Installation
You must compile the library with Metal support enabled:

Bash
CMAKE_ARGS="-DGGML_METAL=on" pip install llama-cpp-python
2. Jupyter Implementation
When loading the model in your notebook, you must load both the MTP GGUF and the mmproj file, while also enabling the MTP draft flag:

Python
from llama_cpp import Llama
from llama_cpp.llama_chat_format import Llava15ChatHandler 

# 1. Load the Vision Projector (The "Eyes")
vision_handler = Llava15ChatHandler(clip_model_path="path/to/mmproj-Qwen3.6-35B-A3B-f16.gguf")

# 2. Load the main MTP model (The "Brain")
# Since you have 256GB VRAM, use the Q8_0 GGUF for maximum precision
llm = Llama(
    model_path="path/to/unsloth/Qwen3.6-35B-A3B-MTP-Q8_0.gguf",
    chat_handler=vision_handler,
    n_ctx=8192,           # Context window
    n_gpu_layers=-1,      # Offload everything to the M3 Ultra GPU
    speculative_draft_n=2 # This enables the MTP speedup
)

# 3. Analyze the image
response = llm.create_chat_completion(
    messages=[
        {
            "role": "user",
            "content": [
                {"type": "image_url", "image_url": {"url": "path_to_your_image.jpg"}},
                {"type": "text", "text": "Analyze this image with maximum precision. Extract all key information."}
            ]
        }
    ]
)

print(response["choices"][0]["message"]["content"])
Can I use Ollama instead?
You can use standard Qwen3.6 in Ollama simply by running ollama run qwen3.6:35b-a3b (which handles downloading the vision projector automatically under the hood). However, standard Ollama pulls the baseline model, not this specific Unsloth MTP variant. To use this exact MTP GGUF in Ollama, you have to download the files manually and build a custom Modelfile that explicitly points to both the local MTP GGUF and the mmproj file.
