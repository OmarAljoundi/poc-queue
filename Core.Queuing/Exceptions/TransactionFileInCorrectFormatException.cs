namespace Core.Queuing.Exceptions
{
    public class TransactionFileInCorrectFormatException(string env, Exception ex) :
        Exception($"transaction.{env}.json has invalid format, see transaction.sample.json for reference.", ex);

}
