import os
import json

joined_members = []
for i in os.listdir("./"):
    if not i.endswith(".json"):
        continue

    with open(i, "r", encoding="utf-8") as f:
        members_info = json.loads(f.read())

    for j in members_info:
        joined_members.append({
            "cardName": j["card"],
            "qqId": j["uin"]
        })

    print(i)

with open("../data/members.json", "w", encoding="utf-8") as f_out:
    f_out.write(json.dumps(joined_members, ensure_ascii=False))
