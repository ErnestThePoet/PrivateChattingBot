import os
import sys

if len(sys.argv) < 2:
    print("Usage: python createfiles.py <number>")
    exit(1)

for i in range(int(sys.argv[1])):
    current_file_name = f"m{i + 1}.json"
    if not os.path.exists(current_file_name):
        with open(current_file_name, "w") as f:
            pass
