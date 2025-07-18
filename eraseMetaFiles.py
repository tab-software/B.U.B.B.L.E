import os

for root, dirs, files in os.walk("."):
    for file in files:
        if file.endswith(".meta"):
            path = os.path.join(root, file)
            os.remove(path)