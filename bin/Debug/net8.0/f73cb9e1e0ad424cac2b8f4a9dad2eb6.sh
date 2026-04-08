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

ps 23774;
while [ $? -eq 0 ];
do
  sleep 1;
  ps 23774 > /dev/null;
done;

for child in $(list_child_processes 23787);
do
  echo killing $child;
  kill -s KILL $child;
done;
rm /Users/reynald/My-Programs/DotnetCore/core8_vue_mysql/bin/Debug/net8.0/f73cb9e1e0ad424cac2b8f4a9dad2eb6.sh;
