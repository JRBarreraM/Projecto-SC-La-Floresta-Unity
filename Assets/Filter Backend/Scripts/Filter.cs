using System.Collections;
using System.Collections.Generic;

public struct Filter {
    public string name;
    public string op;
    public string val;

    public Filter(string name, string op, string val) {
        this.name = name;
        this.op = op;
        this.val = val;
    }

    public string AsString(){
        return name + op + val;
    }
}
