using System.Drawing;

Withdraw withdraw = new Withdraw(500000, "A584A-566AA-VASF56-889REQ-54987Q", "x34597asf59f3zz59f*a6z");

Employee veznedar = new Veznedar();
Employee yonetici = new Yonetici();
Employee mudur = new Mudur();
Employee bolgeSorumlusu = new BolgeSorumlusu();

veznedar.SetNextApprover(yonetici);
yonetici.SetNextApprover(mudur);
mudur.SetNextApprover(bolgeSorumlusu);

veznedar.ProcessRequest(withdraw);

public class Withdraw
{
    public decimal Amount { get; }
    public string ProcessId { get; }
    public string Account { get; }

    public Withdraw(decimal Amount, string ProcessId, string Account)
    {
        this.Amount = Amount;
        this.ProcessId = ProcessId;
        this.Account = Account;
    }
}

public abstract class Employee
{
    protected Employee NextApprover;
    public void SetNextApprover(Employee supervisor)
    {
        this.NextApprover = supervisor;
    }

    public abstract void ProcessRequest(Withdraw req);

}

public class Veznedar : Employee
{
    public override void ProcessRequest(Withdraw req)
    {
        if (req.Amount <= 4000)
        {
            Console.WriteLine($"İşlem {this.GetType().Name} tarafından onaylandı #{req.Amount}");
        }
        else if (NextApprover != null)
        {
            Console.WriteLine($"{req.Amount} işlem tutarı max. limiti aştığından yöneticiye yönlendirildi.");
            NextApprover.ProcessRequest(req);
        }
    }
}

public class Yonetici : Employee
{
    public override void ProcessRequest(Withdraw req)
    {
        if (req.Amount <= 50000)
        {
            Console.WriteLine($"İşlem {this.GetType().Name} tarafından onaylandı #{req.Amount}");
        }
        else if (NextApprover != null)
        {
            Console.WriteLine($"{req.Amount} işlem tutarı max. limiti aştığından müdüre yönlendirildi.");
            NextApprover.ProcessRequest(req);
        }
    }
}

public class Mudur : Employee
{
    public override void ProcessRequest(Withdraw req)
    {
        if (req.Amount <= 100000)
        {
            Console.WriteLine($"İşlem {this.GetType().Name} tarafından onaylandı #{req.Amount}");
        }
        else if (NextApprover != null)
        {
            Console.WriteLine($"{req.Amount} işlem tutarı max. limiti aştığından bölge sorumlusuna yönlendirildi.");
            NextApprover.ProcessRequest(req);
        }
    }
}

public class BolgeSorumlusu : Employee
{
    public override void ProcessRequest(Withdraw req)
    {
        if (req.Amount <= 1000000)
        {
            Console.WriteLine($"İşlem {this.GetType().Name} tarafından onaylandı #{req.Amount}");
        }
        else
        {
            throw new Exception($"Limit banka günlük işlem limitini aştığından para çekme işlemi #{req.Amount} onaylanmadı.");
        }
    }
}
