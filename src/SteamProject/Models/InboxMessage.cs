﻿using System;
using System.Collections.Generic;

namespace SteamProject.Models;

public partial class InboxMessage
{
    public int Id { get; set; }

    public int RecipientId { get; set; }
    public int? SenderId { get; set; }

    public DateTime? TimeStamp { get; set; }

    public string? Sender { get; set; }

    public string? Subject { get; set; }

    public string? Content { get; set; }

    public virtual User Recipient { get; set; } = null!;
}
