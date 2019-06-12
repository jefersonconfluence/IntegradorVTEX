using System;
using System.Diagnostics;

namespace IntegradorAticOs.DAL
{
    public class LogDAL
    {
        const string LOG_NAME = "IntegradorAticOs"; //Nome da entrada no Visualizador de Eventos do Windows 
        const string SOURCE = "IntegradorAticOs"; //Nome da fonte (source) com que serão gravados os logs. 

            public LogDAL()
            {
                //verifica se o log já existe, se não existe então cria;  
                if (EventLog.SourceExists(SOURCE) == false)
                    EventLog.CreateEventSource(SOURCE, LOG_NAME);
            }

            public void WriteEntry(string input, EventLogEntryType entryType)
            {
                //grava o texto na fonte de logs com o nome que      definimos para a constante SOURCE.  
                EventLog.WriteEntry(SOURCE, input, entryType);
            }

            public void WriteEntry(string input)
            {
                //loga um simples evento com a categoria de informação.  
                WriteEntry(input, EventLogEntryType.Information);
            }

            public void WriteEntry(Exception ex)
            {
                //loga a ocorrência de uma excessão com a categoria de erro.  
                WriteEntry(ex.ToString(), EventLogEntryType.Error);
            }
    }
}
