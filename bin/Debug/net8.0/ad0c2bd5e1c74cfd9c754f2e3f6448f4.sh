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

ps 5683;
while [ $? -eq 0 ];
do
  sleep 1;
  ps 5683 > /dev/null;
done;

for child in $(list_child_processes 5685);
do
  echo killing $child;
  kill -s KILL $child;
done;
rm /Users/reynald/My-Programs/DotnetCore/core8_vue_mysql/bin/Debug/net8.0/ad0c2bd5e1c74cfd9c754f2e3f6448f4.sh;
