using com.TresToGames.TrainersApp.BO_SuperClasses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRoutineOutDTO : OutDTO
{
    public NewRoutineOutDTO(Dictionary<string, string> info) : base(info)
    {
    }

    public NewRoutineOutDTO(TrainerClientRelation trainerClientRelationToSave) : base(new Dictionary<string, string>())
    {
        fields.Add("trainerClientRelationId", trainerClientRelationToSave.Id.ToString());
        fields.Add("clientId", trainerClientRelationToSave.Client.Id.ToString());
        fields.Add("numOfDays", trainerClientRelationToSave.Routine.NumberOfDays.ToString());
        fields.Add("startDate", trainerClientRelationToSave.Routine.StartDate.ToString(Constant.DEFAULT_SIMPLE_API_DATE_FORMAT));
        Daily[] daa = trainerClientRelationToSave.Routine.DailyActivities.ToArray();
        for (int i = 0; i < daa.Length; i++)
        {
            fields.Add("dailyActivities[" + i + "].dayNumber", daa[i].DayNumber.ToString());
            fields.Add("dailyActivities[" + i + "].proposedDate", daa[i].ProposedDate.ToString(Constant.DEFAULT_SIMPLE_API_DATE_FORMAT));
            Activity[] aa = daa[i].Activities.ToArray();
            for(int j = 0; j < aa.Length; j++)
            {
                fields.Add("dailyActivities[" + i + "].activities[" + j + "].orderNumber",aa[j].OrderNumber.ToString());
                fields.Add("dailyActivities[" + i + "].activities[" + j + "].actTypeId", aa[j].ActTypeId.ToString());
                fields.Add("dailyActivities[" + i + "].activities[" + j + "].actTypeValue",aa[j].TypeValue.ToString());
                fields.Add("dailyActivities[" + i + "].activities[" + j + "].trainingId",aa[j].Training.Id.ToString());
            }
        }
    }
}
