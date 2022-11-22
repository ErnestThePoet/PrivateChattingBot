import os
import json

with open("full_cardnames.txt", "r", encoding="utf-8") as f:
    full_cardnames = set([x.replace("?", "").replace(" ", "") for x in f.read().splitlines()])

if "" in full_cardnames:
    full_cardnames.remove("")

for i in os.listdir("./"):
    if not i.endswith(".json"):
        continue

    with open(i, "r", encoding="utf-8") as f:
        members_info = json.loads(f.read())

    for j in members_info:
        found_full_card_names = False

        purified_cardname = \
            j["card"] \
                .replace("?", "") \
                .replace("&nbsp;", "") \
                .replace(" ", "") \
                .replace(" ", "")

        for k in full_cardnames:
            if k.startswith(purified_cardname):
                j["card"] = k
                found_full_card_names = True
                break

        if not found_full_card_names:
            print(f"No full cardname found for {purified_cardname}")

    with open(i, "w", encoding="utf-8") as f_out:
        f_out.write(json.dumps(members_info, ensure_ascii=False))
