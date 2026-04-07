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

ps 77434;
while [ $? -eq 0 ];
do
  sleep 1;
  ps 77434 > /dev/null;
done;

for child in $(list_child_processes 77739);
do
  echo killing $child;
  kill -s KILL $child;
done;
rm /Users/reynald/My-Programs/DotnetCore/core8_vue_mysql/bin/Debug/net8.0/eaaaf5023b2f4ca0a736c9d0b4675f6e.sh;
