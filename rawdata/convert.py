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

    print(f"{len(members_info)} from {i}")

for i, ie in enumerate(joined_members):
    for j in joined_members[i + 1:]:
        if ie["cardName"] != "" and ie["cardName"] == j["cardName"]:
            print(f"Duplication detected: "
                  f"{ie['cardName']}({ie['qqId']}) and {j['cardName']}({j['qqId']})")

with open("../data/members.json", "w", encoding="utf-8") as f_out:
    f_out.write(json.dumps(joined_members, ensure_ascii=False))

print(f"A total of {len(joined_members)} is written.")
