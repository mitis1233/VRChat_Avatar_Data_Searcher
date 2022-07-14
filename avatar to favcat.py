import json
with open('Z:\\A.txt', 'r', encoding='utf-8') as f:
    output = json.load(f)

count=0
Temp=""
Data=""

while count<=len(output)-1: 
    Temp=f"{output[count]['Id']} {output[count]['AvatarName']}\n"
    count+=1
    Data+=Temp
    print(count)
    
print(Data)
with open('Z:\\avatars_1.txt', 'w', encoding='utf-8') as f:
    f.writelines(Data)