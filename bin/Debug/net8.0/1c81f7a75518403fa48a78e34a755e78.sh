function list_child_processes () {
    local ppid=$1;
    local current_children=$(pgrep -P $ppid);
    local local_child;
    if [ $? -eq 0 ];
    then
        for current_child in $current_children
        do
          local_child=$current_child;
          list_child_processes $local_child;
          echo $local_child;
        done;
    else
      return 0;
    fi;
}

ps 86048;
while [ $? -eq 0 ];
do
  sleep 1;
  ps 86048 > /dev/null;
done;

for child in $(list_child_processes 86053);
do
  echo killing $child;
  kill -s KILL $child;
done;
rm /Users/reynald/My-Programs/DotnetCore/core8_vue_mysql/bin/Debug/net8.0/1c81f7a75518403fa48a78e34a755e78.sh;
