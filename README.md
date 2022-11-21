2022年11月担任2019级辅导员助理期间为核酸检测提醒而设计。  
### 使用方法
准备好配置文件`data/config.json`（仓库已提供）和`data/members.json`。`data/members.json`文件格式为`Array<{cardName:string;qqId:number;}>`，存储QQ群中所有成员的群名片和QQ号。打开程序后，在界面左上方输入框中输入所有要发起私聊的成员的群名片（完整的或一部分），每行一个；在界面下方的输入框中输入私聊发送的消息。然后打开QQ群界面并最大化，点击群成员搜索按钮使搜索框出现。按`Caps Lock`键启动程序。程序运行过程中可以随时按下`` ` ``（数字键盘1键左侧）键停止程序运行，之后可以继续按`Caps Lock`键来继续未完成的私聊。如果程序点击位置不准确，请您根据界面实际位置修改`data/config.json`文件中的配置，更新点击坐标。
### 基本原理
按`Caps Lock`键启动程序后，程序首先读取界面左上方输入框中每一行中的私聊目标姓名，在群成员数据库(`data/members.json`)中进行查找，如果有某一群成员的群名片中包含该目标姓名，则将其添加到私聊任务列表中。然后，程序以模拟键鼠操作的形式，循环执行每一个私聊任务。对于每一个私聊任务，程序会自动点击QQ群搜索框，在剪切板上设置当前私聊目标的QQ号，发送`Ctrl+V`按键事件将其粘贴进搜索框，随后按下`Enter`键打开私聊窗口，并在剪切板上设置私聊消息。程序点击聚焦私聊窗口的消息输入框，发送`Ctrl+A`和`Ctrl+V`按键事件将私聊消息粘贴入消息输入框（由于消息输入框中可能保留着之前未发送的消息，按下`Ctrl+A`可确保在粘贴时已有消息被全选，可将已有消息覆盖）。然后程序发送`Ctrl+Enter`按键事件发送消息，之后发送`Alt+F4`按键事件关闭当前私聊窗口，进入下一次私聊。每个操作之间都设置了一定时间间隔，确保在QQ界面稳定后再进行操作。时间间隔同样可以在`data/config.json`文件中配置。